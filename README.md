# Microsoft Azure Function usando RabbitMQ

Existem varios tipos de functions e cada uma pode ser acionnada de maneira diferente. Queremos mostrar aqui o uso dois tipos de acionamentos, **Http** e **Queue**. 

![alt text](https://github.com/martinhosebastiao/AzureFunctionWithRabbitMQ/blob/main/AzureFunctionRabbitMQ.png?raw=true)


# Configuração do RabbitMQ - Docker Compose
```yaml
version: "3.2"
services:
  rabbitmq:
    image: rabbitmq:alpine
    container_name: "rabbitmq"
    ports:
      - 5672:5672
      - 15672:15672
    volumes:
      - ~/.docker-conf/rabbitmq/data/:/var/lib/rabbitmq/
      - ~/.docker-conf/rabbitmq/log/:/var/log/rabbitmq
    networks:
      - rabbitmq
    restart: unless-stopped

networks:
  rabbitmq:
    driver: bridge
```
