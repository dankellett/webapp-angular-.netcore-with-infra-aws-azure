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

variable "auth_cert_password" {
  type        = string
  description = "The password for the auth certificate to be installed."
}

variable "auth_cert_filepath" {
  type        = string
  description = "The full path for the auth certificate to be installed."
  default     = "../auth_key_dev.pfx"
}

variable "auth_cert_thumbprint" {
  type        = string
  description = "The thumbprint of auth certificate to be installed - the applicaiton uses this to find the cert in the cert store."
}

variable "app_global_admin_username" {
  type        = string
  description = "The username for the application global admin user. It must be an email."
}

variable "app_global_admin_password" {
  type        = string
  description = "The password for the application global admin user. It must meet typical strength properties."
}
