TODO:node_module permissions.
TODO: Docker image versions
#Docker

allow for docker to be run as user
* sudo usermod -a -G docker dave                                                                                
##Commands
###All
* sudo docker-compose start (start all background)
###Vue
* sudo docker-compose exec ui sh

###dotnet
* sudo docker-compose exec ui sh

#EF

* Migration
  * dotnet ef migrations add MigrationName
  * dotnet ef database update
