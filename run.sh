#!/bin/bash

#TODO: find a way to run container as a user thats not root
# Allows for conditionally setting the comand variable
case $1 in

    api )
        case $2 in

            watch ) 
                export CMD="sh -c 'dotnet watch --project ./src/IrrigationSystem.csproj run --no-restore'"
                docker-compose up api ;;

            test ) 
                export CMD="sh -c 'dotnet watchdotnet watch --project ./src/IrrigationSystem.csproj test --no-restore'"
                docker-compose run api ;;

            sh ) 
                export CMD=bash
                docker-compose run api 
                sudo chown -R $(id -u):$(id -g) ./api ;;

            build ) 
                export CMD="sh -c 'dotnet build'"
                docker-compose build api
                sudo chown -R $(id -u):$(id -g) ./api ;;
            * ) 
                echo "Invalid commmand."
                echo "Commands:"
                echo "[sh | watch | test | build]" ;;

        esac ;;

    ui )
        docker-compose up web ;;
    * )
        echo "Usage:"
        echo "./run.sh [component] [command]"
        echo ""
        echo "Components:"
        echo "[ui | api]"
        echo ""
        echo "Commands:"
        echo "api:"
        echo "    [sh | watch | test | build]"
        echo " ui:"
        echo "    []" ;;
esac




