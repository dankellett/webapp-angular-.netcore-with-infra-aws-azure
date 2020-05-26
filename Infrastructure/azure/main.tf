# Define Terraform provider
terraform {
  required_version = ">= 0.12"
}
# Configure the Azure provider
provider "azurerm" { 
  environment = "public"
  version = "=2.9.0"
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.resource_location
}

resource "azurerm_storage_account" "rg" {
  name                     = var.storage_account_name
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_sql_server" "rg" {
  name                         = var.database_server_name
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.database_admin_username
  administrator_login_password = var.database_admin_password

  extended_auditing_policy {
    storage_endpoint                        = azurerm_storage_account.rg.primary_blob_endpoint
    storage_account_access_key              = azurerm_storage_account.rg.primary_access_key
    storage_account_access_key_is_secondary = true
    retention_in_days                       = 6
  }

  tags = {
    environment = "production"
  }
}

resource "azurerm_sql_firewall_rule" "rg" {
  name                = "AllowAllAzureIps"
  resource_group_name = azurerm_resource_group.rg.name
  server_name         = azurerm_sql_server.rg.name
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_sql_database" "rg" {
  name                = var.database_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  server_name         = azurerm_sql_server.rg.name

  extended_auditing_policy {
    storage_endpoint                        = azurerm_storage_account.rg.primary_blob_endpoint
    storage_account_access_key              = azurerm_storage_account.rg.primary_access_key
    storage_account_access_key_is_secondary = true
    retention_in_days                       = 6
  }

  tags = {
    environment = "production"
  }
}

resource "azurerm_app_service_plan" "rg" {
  name                = var.app_service_plan_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "rg" {
  name                = var.app_service_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.rg.id

  site_config {
    dotnet_framework_version = "v4.0"
    scm_type                 = "None"
  }

  app_settings = {
    "app_global_admin_username" = var.app_global_admin_username
    "app_global_admin_password" = var.app_global_admin_password
    "auth_cert_thumbprint" = var.auth_cert_thumbprint
    "WEBSITE_LOAD_CERTIFICATES" = "*"
  }

  connection_string {
    name  = "DefaultConnection"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_sql_server.rg.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.rg.name};Persist Security Info=False;User ID=${azurerm_sql_server.rg.administrator_login};Password=${azurerm_sql_server.rg.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

resource "azurerm_app_service_certificate" "rg" {
  name                = "auth-cert"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  pfx_blob            = filebase64(var.auth_cert_filepath)
  password            = var.auth_cert_password
}