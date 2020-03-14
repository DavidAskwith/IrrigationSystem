TODO:node_module permissions.
TODO: Docker image versions check updates
TODO: SQL server package dev only

#Docker

allow for docker to be run as user
* sudo usermod -a -G docker dave                                                                                

###dotnet

* After removal of a db and bin a restore must be run not just a image rebuild
  * why Are volumes mounted later in the life cycle?

#EF

* Migration
  * dotnet ef migrations add MigrationName
  * dotnet ef database update

