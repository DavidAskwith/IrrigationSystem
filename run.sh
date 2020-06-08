#!/bin/bash

#TODO: find a way to run container as a user thats not root
#TODO: add double project watch

# run all as default
if [ -z "$2" ] ;
then
    if [ "$1" == "watch" ] ;
    then
        export apiCMD="sh -c 'dotnet watch --project ./src/IrrigationSystem.csproj run --no-restore'"
        export uiCMD="sh -c 'npm run serve'"

        docker-compose up
        exit
    elif [ -z "$1" ] ;
    then
        export apiCMD="sh -c 'dotnet run --project ./src/IrrigationSystem.csproj run --no-restore'"
        export uiCMD="sh -c 'npm run serve'"

        docker-compose up
        exit
    fi
fi

# Allows for conditionally setting the comand variable
case $1 in

    api )
        case $2 in

            watch )
                export apiCMD="sh -c 'dotnet watch --project ./src/IrrigationSystem.csproj run --no-restore'"
                docker-compose up api ;;

            test )
                export apiCMD="sh -c 'dotnet watch --project ./test/Test.csproj test --no-restore'"
                docker-compose run api ;;

            sh )
                export apiCMD=bash
                docker-compose run api
                sudo chown -R $(id -u):$(id -g) ./api ;;

            build )
                export apiCMD="sh -c 'dotnet build'"
                #TODO: Do I want to build docker
                docker-compose run api
                sudo chown -R $(id -u):$(id -g) ./api ;;
            * )
                echo "Invalid commmand."
                echo "Commands:"
                echo "[ sh | watch | test | build ]" ;;

        esac ;;

    ui )
        case $2 in
            sh )
                export uiCMD=sh
                docker-compose run ui ;;
            build )
                export uiCMD="sh -c 'npm run build'"
                docker-compose up ui ;;
            test )
                export uiCMD="sh -c 'npm run test:unit'"
                docker-compose up ui ;;
            watch )
                export uiCMD="sh -c 'npm run serve'"
                docker-compose up ui ;;

            * )
                echo "Invalid commmand."
                echo "Commands:"
                echo "[ sh | watch | test | build ]" ;;
        esac ;;

    * )
        echo "Usage:"
        echo ""
        echo "All:"
        echo "./run.sh"
        echo "[ watch | ]"
        echo ""
        echo "Specific Component:"
        echo "./run.sh [component] [command]"
        echo ""
        echo "Components:"
        echo "[ ui | api ]"
        echo ""
        echo "Commands:"
        echo "api:"
        echo "[ sh | watch | test | build ]"
        echo " ui:"
        echo "[ sh | watch | test | build ]" ;;
esac




