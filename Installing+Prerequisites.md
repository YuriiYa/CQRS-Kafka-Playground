# Course reference
https://ciklum.udemy.com/course/net-microservices-cqrs-event-sourcing-with-kafka/learn/lecture/29894412#overview 

#1. .NET 6 SDK

https://dotnet.microsoft.com/en-us/download/dotnet/6.0

#2. IDE or Code Editor

VS Code:
https://code.visualstudio.com/download

Visual Studio Community Edition:
https://visualstudio.microsoft.com/vs/community/

Rider:
https://www.jetbrains.com/rider/

#4. VS Code Extensions

C# for Visual Studio Code:
https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

NuGet Package Manager:
https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager

SQL Server (mssql):
https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql

#5. Postman

Download from:
https://www.postman.com/downloads/

#6. Docker

Download for Mac or Windows:
https://www.docker.com/products/docker-desktop

Download for Linux Ubuntu:
https://docs.docker.com/engine/install/ubuntu/

Download for Linux Debian:
https://docs.docker.com/engine/install/debian/

Download for Linux CentOS:
https://docs.docker.com/engine/install/centos/

Download for Linux Fedora:
https://docs.docker.com/engine/install/fedora/

Once installed, check Docker version:
> docker --version

#7. Create Docker Network - techbankNet 

docker network create --attachable -d bridge mydockernetwork

#8. Install or init docker compose 

https://docs.docker.com/compose/install

#9. Apache Kafka

Create docker-compose.yml file with contents:

version: "3.4"

services:
  zookeeper:
    image: bitnami/zookeeper
    restart: always
    ports:
      - "2181:2181"
    volumes:
      - "zookeeper_data:/bitnami"
    environment:
      - ALLOW_ANONYMOUS_LOGIN=yes
  kafka:
    image: bitnami/kafka
    ports:
      - "9092:9092"
    restart: always
    volumes:
      - "kafka_data:/bitnami"
    environment:
      - KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181
      - ALLOW_PLAINTEXT_LISTENER=yes
      - KAFKA_LISTENERS=PLAINTEXT://:9092
      - KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092
    depends_on:
      - zookeeper

volumes:
  zookeeper_data:
    driver: local
  kafka_data:
    driver: local
   
networks:
  default:
    external:
      name: mydockernetwork
    

The run by executing the following command:

> docker-compose up -d

#9. MongoDB

Run in Docker:
docker run -it -d --name mongo-container -p 27017:27017 --network mydockernetwork --restart always -v mongodb_data_container:/data/db mongo:latest

Download Client Tools – Robo 3T:
https://robomongo.org/download

#9. Microsoft SQL Server

docker run -d --name sql-container \
--network mydockernetwork \
--restart always \
-e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=$tr0ngS@P@ssw0rd02' -e 'MSSQL_PID=Express' \
-p 1433:1433 mcr.microsoft.com/mssql/server:2017-latest-ubuntu 


docker run -d --name sql-container --network mydockernetwork --restart always -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=$tr0ngS@P@ssw0rd02' -e 'MSSQL_PID=Express' -p 1433:1433 mcr.microsoft.com/mssql/server:2017-latest-ubuntu 

docker logs sql-container

  "ConnectionStrings":{
    "SqlServer": "Server=localhost,1433;Database=SocialMedia;User Id=SMUser;Password=SmPA$$06500"
  }
}