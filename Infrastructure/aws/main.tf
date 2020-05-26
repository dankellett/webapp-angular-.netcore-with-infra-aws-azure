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

  name     = var.database_name
  username = var.database_user
  password = var.database_password
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
  subnet_ids = module.subnets.private_subnet_ids

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

# module "rds_instance" {
#   source              = "git::https://github.com/cloudposse/terraform-aws-rds.git?ref=tags/0.9.4"
#   namespace           = var.global_namespace
#   stage               = var.global_stage
#   name                = var.global_name
#   database_name       = var.database_name
#   database_user       = var.database_user
#   database_password   = var.database_password
#   allocated_storage   = var.database_allocated_storage
#   database_port       = var.database_port
#   db_parameter_group  = var.database_parameter_group
#   engine              = var.database_engine
#   engine_version      = var.database_engine_version
#   multi_az            = false
#   storage_type        = "standard"
#   instance_class      = var.database_instance_class
#   storage_encrypted   = false
#   publicly_accessible = false
#   vpc_id              = module.vpc.vpc_id
#   subnet_ids          = module.subnets.private_subnet_ids
#   security_group_ids  = [module.vpc.vpc_default_security_group_id]
#   apply_immediately   = true

#   # db_parameter = [
#   #   {
#   #     name         = "myisam_sort_buffer_size"
#   #     value        = "1048576"
#   #     apply_method = "immediate"
#   #   },
#   #   {
#   #     name         = "sort_buffer_size"
#   #     value        = "2097152"
#   #     apply_method = "immediate"
#   #   }
#   # ]
# }

module "elastic_beanstalk_application" {
  source      = "git::https://github.com/cloudposse/terraform-aws-elastic-beanstalk-application.git?ref=tags/0.5.0"
  namespace   = var.global_namespace
  stage       = var.global_stage
  name        = var.global_name
  description = "Template Application Description"
}

module "elastic_beanstalk_environment" {
  source                     = "git::https://github.com/cloudposse/terraform-aws-elastic-beanstalk-environment.git?ref=tags/0.19.0"
  namespace                  = var.global_namespace
  stage                      = var.global_stage
  name                       = var.global_name
  description                = "Template Application Environment Description"
  region                     = var.global_region
  autoscale_min              = 1
  autoscale_max              = 1
  instance_type              = "t2.micro"
  availability_zone_selector = "Any"
  updating_min_in_service    = 0
  updating_max_batch         = 1
  root_volume_size           = 30

  elastic_beanstalk_application_name = module.elastic_beanstalk_application.elastic_beanstalk_application_name

  vpc_id                  = module.vpc.vpc_id
  loadbalancer_subnets    = module.subnets.public_subnet_ids
  application_subnets     = module.subnets.private_subnet_ids
  allowed_security_groups = [module.vpc.vpc_default_security_group_id]

  // https://docs.aws.amazon.com/elasticbeanstalk/latest/platforms/platforms-supported.html
  // https://docs.aws.amazon.com/elasticbeanstalk/latest/platforms/platforms-supported.html#platforms-supported.docker
  solution_stack_name = var.beanstalk_solution_stack_name

  additional_settings = [
    { namespace = "aws:elasticbeanstalk:application:environment", name = "auth_cert_thumbprint", value = var.auth_cert_thumbprint },
    { namespace = "aws:elasticbeanstalk:application:environment", name = "app_global_admin_username", value = var.app_global_admin_username },
    { namespace = "aws:elasticbeanstalk:application:environment", name = "app_global_admin_password", value = var.app_global_admin_password },
    # { namespace = "aws:rds:dbinstance",name = "DBEngine", value = "sqlserver-web" },
    # { namespace = "aws:rds:dbinstance",name = "DBInstanceClass", value = "db.t2.micro" },
    # { namespace = "aws:rds:dbinstance",name = "DBUser", value = "dkellett" },
    # { namespace = "aws:rds:dbinstance",name = "DBPassword", value = "kaskaD1e-0" },
    # { namespace = "aws:rds:dbinstance",name = "MultiAZDatabase", value = "false" }
  ]
}