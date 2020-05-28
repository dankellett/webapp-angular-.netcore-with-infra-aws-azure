provider "aws" {
  region = var.global_region
}

module "vpc" {
  source     = "git::https://github.com/cloudposse/terraform-aws-vpc.git?ref=tags/0.10.0"
  namespace  = var.global_namespace
  stage      = var.global_stage
  name       = var.vpc_name
  cidr_block = var.vpc_cidr
}

module "subnets" {
  source               = "git::https://github.com/cloudposse/terraform-aws-dynamic-subnets.git?ref=tags/0.19.0"
  namespace            = var.global_namespace
  stage                = var.global_stage
  name                 = var.global_name
  availability_zones   = var.vpc_subnet_availability_zones
  vpc_id               = module.vpc.vpc_id
  igw_id               = module.vpc.igw_id
  cidr_block           = module.vpc.vpc_cidr_block
  nat_gateway_enabled  = true
  nat_instance_enabled = false
}

module "db" {
  source  = "terraform-aws-modules/rds/aws"
  version = "~> 2.0"

  identifier = "demodb"

  engine            = var.database_engine
  engine_version    = var.database_engine_version
  instance_class    = var.database_instance_class
  allocated_storage = var.database_allocated_storage

  name     = "" #must be empty for sqlserver
  username = var.database_admin_username
  password = var.database_admin_password
  port     = var.database_port

  #iam_database_authentication_enabled = true

  vpc_security_group_ids = [module.vpc.vpc_default_security_group_id]

  maintenance_window = "Mon:00:00-Mon:03:00"
  backup_window      = "03:00-06:00"

  # Enhanced Monitoring - see example for details on how to create the role
  # by yourself, in case you don't want to create it automatically
  #monitoring_interval = "30"
  #monitoring_role_name = "MyRDSMonitoringRole"
  create_monitoring_role = false

  tags = {
    Owner       = "user"
    Environment = "dev"
  }

  # DB subnet group
  #subnet_ids = module.subnets.private_subnet_ids
  subnet_ids = module.subnets.public_subnet_ids

  # DB parameter group
  family = "sqlserver-web-14.0"

  # DB option group
  major_engine_version = "14.00"

  # Snapshot name upon DB deletion
  final_snapshot_identifier = "demodb"

  # Database Deletion Protection
  deletion_protection = false

  # parameters = [
  #   {
  #     name = "character_set_client"
  #     value = "utf8"
  #   },
  #   {
  #     name = "character_set_server"
  #     value = "utf8"
  #   }
  # ]

  # options = [
  #   {
  #     option_name = "MARIADB_AUDIT_PLUGIN"

  #     option_settings = [
  #       {
  #         name  = "SERVER_AUDIT_EVENTS"
  #         value = "CONNECT"
  #       },
  #       {
  #         name  = "SERVER_AUDIT_FILE_ROTATIONS"
  #         value = "37"
  #       },
  #     ]
  #   },
  # ]
}

module "elastic_beanstalk_application" {
  source      = "git::https://github.com/cloudposse/terraform-aws-elastic-beanstalk-application.git?ref=tags/0.5.0"
  namespace   = var.global_namespace
  stage       = var.global_stage
  name        = var.global_name
  description = "Template Application Description"
}

module "elastic_beanstalk_environment" {
  source                      = "git::https://github.com/cloudposse/terraform-aws-elastic-beanstalk-environment.git?ref=tags/0.19.0"
  namespace                   = var.global_namespace
  stage                       = var.global_stage
  name                        = var.global_name
  description                 = "Template Application Environment Description"
  region                      = var.global_region
  autoscale_min               = 1
  autoscale_max               = 1
  instance_type               = "t2.micro"
  availability_zone_selector  = "Any"
  updating_min_in_service     = 0
  updating_max_batch          = 1
  root_volume_size            = 30
  associate_public_ip_address = true


  elastic_beanstalk_application_name = module.elastic_beanstalk_application.elastic_beanstalk_application_name

  vpc_id                  = module.vpc.vpc_id
  loadbalancer_subnets    = module.subnets.public_subnet_ids
  application_subnets     = module.subnets.public_subnet_ids
  allowed_security_groups = [module.vpc.vpc_default_security_group_id]

  // https://docs.aws.amazon.com/elasticbeanstalk/latest/platforms/platforms-supported.html
  // https://docs.aws.amazon.com/elasticbeanstalk/latest/platforms/platforms-supported.html#platforms-supported.docker
  solution_stack_name = var.beanstalk_solution_stack_name

  additional_settings = [
    { namespace = "aws:elasticbeanstalk:application:environment", name = "auth_cert_thumbprint", value = var.auth_cert_thumbprint },
    { namespace = "aws:elasticbeanstalk:application:environment", name = "app_global_admin_username", value = var.app_global_admin_username },
    { namespace = "aws:elasticbeanstalk:application:environment", name = "app_global_admin_password", value = var.app_global_admin_password },
  ]
}