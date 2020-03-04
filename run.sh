#!/bin/bash

#TODO: find a way to run container as a user thats not root
# Allows for conditionally setting the comand variable
case $1 in

    api )
        case $2 in

            watch ) 
                export CMD="sh -c 'dotnet watch run --no-restore'"
                docker-compose up api ;;

            test ) 
                export CMD="sh -c 'dotnet watch test --no-restore'"
                docker-compose run api ;;

            sh ) 
                export CMD=sh
                docker-compose run api 
                sudo chown -R $(id -u):$(id -g) ./api ;;

            build ) 
                docker-compose build api
                sudo chown -R $(id -u):$(id -g) ./api ;;
        esac ;;

    ui )
        docker-compose up web ;;
esac




