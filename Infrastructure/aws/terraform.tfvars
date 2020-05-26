global_region       = "us-west-2"
global_namespace    = "template_namespace"
global_name         = "template_app"
global_stage        = "dev"

vpc_name = "template_vpc"
vpc_cidr = "10.0.0.0/16"

vpc_subnet_availability_zones = ["us-west-2a", "us-west-2b"]

database_name = ""
database_user = "dkellett"
database_password = "kaskaD3-"
database_port = 1433
database_engine = "sqlserver-web"
database_engine_version = "14.00.3281.6.v1"
database_allocated_storage = 20
database_instance_class = "db.t2.small"
database_parameter_group = "default"

beanstalk_availability_zones = ["us-west-2a"]
beanstalk_solution_stack_name = "64bit Windows Server Core 2019 v2.5.6 running IIS 10.0"
