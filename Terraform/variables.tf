variable "resource_group_name" {
  type        = string
  description = "The name of the resorce group for all resources in this project."
}
variable "resource_location" {
  type        = string
  description = "The Azure region which all resources will be created in."
}
variable "database_server_name" {
  type        = string
  description = "The idetifier to be given to the Azure SQL Server."
}
variable "database_name" {
  type        = string
  description = "The idetifier to be given to the Azure SQL database."
}

variable "database_admin_username" {
  type        = string
  description = "The username to be set for the database administrator."
}

variable "database_admin_password" {
  type        = string
  description = "The password to be set for the database administrator."
}

variable "app_service_name" {
  type        = string
  description = "The name to be used for the Azure App Service. This must be unique to all Azure and is exposed on the app URL."
}

variable "app_service_plan_name" {
  type        = string
  description = "The name to be used for the Azure App Service Plan."
}

variable "storage_account_name" {
  type        = string
  description = "The name to be used for the Azure App Service. This must be unique to all Azure and consist of lowercase characters."
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

