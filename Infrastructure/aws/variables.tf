variable global_region {
  type = string
}

variable global_namespace {
  type = string
}

variable global_name {
  type = string
}

variable global_stage {
  type = string
}

variable vpc_name {
  type = string
}

variable vpc_cidr {
  type = string
}

variable vpc_subnet_availability_zones {
  type = list(string)
}

variable beanstalk_availability_zones {
  type = list(string)
}

variable beanstalk_solution_stack_name {
  type = string
}

variable database_name {
  type = string
}
variable database_user {
  type = string
}
variable database_password {
  type = string
}
variable database_port {
  type = number
}
variable database_engine {
  type = string
}
variable database_engine_version {
  type = string
}
variable database_allocated_storage {
  type = number
}
variable database_instance_class {
  type = string
}
variable database_parameter_group {
  type = string
}
