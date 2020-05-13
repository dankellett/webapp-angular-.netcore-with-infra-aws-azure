# Angular & .NET Core 3.1 Sample Project
This repository contains a project that is intended as a starting point for a web-application based on a .NET Core 3.1 API and Angular based front-end.

Authenticaiton is provided by IdentityServer4.

Database abstraction is provided by EF Core 3.1.

#### Getting Started
To run the project run `npm start` in the `.\ClientApp` folder and `dotnet run` in the root folder - in that order. This will allow Angular development in front-end or server without having to rebuild the other.

#### Database and Entity Framework

#### Angular Client App

#### Configuration
##### Identity Server
IdentityServer has discrete settings for development and production configuration. In a development config, IS4 will use the local certificate file `auth_key_dev.pfx`. In production, IS4 will look for the same certificate in the cert store on the deployed machine.

Settings for the certificate thumbrint `auth_cert_thumbprint` (prod & dev) and cert password `auth_cert_password` (dev only) are required.

Only a self signed cert is required for idenity. The OpenSSL CLI tool can be used to generate this file

###### Create Azure compliant cert
`openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout example.key -out example.crt -subj "/CN=example.com" -days 3650`

###### Package pfx
`openssl pkcs12 -export -out auth_cert.pfx -inkey example.key -in example.crt`

Use this command to extract thumbprint to update the startup.cs

`Get-PfxCertificate -FilePath Certificate.pfx`

#### Azure Pipelines

`-var="app_global_admin_password=$(app_global_admin_password)" -var="app_global_admin_username=$(app_global_admin_username)" -var="app_service_name=$(app_service_name)" -var="app_service_plan_name=$(app_service_plan_name)"  -var="auth_cert_filepath=$(auth_cert_filepath)" -var="auth_cert_password=$(auth_cert_password)" -var="auth_cert_thumbprint=$(auth_cert_thumbprint)" -var="database_admin_password=$(database_admin_password)" -var="database_admin_username=$(database_admin_username)" -var="database_name=$(database_name)" -var="database_server_name=$(database_server_name)" -var="resource_group_name=$(resource_group_name)" -var="resource_location=$(resource_location)" -var="storage_account_name=$(storage_account_name)"`



