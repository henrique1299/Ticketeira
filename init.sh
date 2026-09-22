#!/bin/sh
apk add --no-cache curl

sleep 60

echo "1. Aguardando Kafka Connect..."
until curl -s http://connect:8083/connectors | grep -q '\['; do
  sleep 3
done

echo "2. Cadastrando Conector Postgres..."
curl -s -X POST http://connect:8083/connectors \
  -H "Content-Type: application/json" \
  -d '{
    "name": "postgres-evento-artista-connector",
    "config": {
      "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
      "tasks.max": "1",
      "database.hostname": "ticketeira-postgres-1",
      "database.port": "5432",
      "database.user": "postgres",
      "database.password": "pF*vitEA0z*xZ-ENF91a",
      "database.dbname": "ticketeira_db",
      "topic.prefix": "ticketeira",
      "table.include.list": "public.evento_artista",
      "plugin.name": "pgoutput",
      "slot.name": "debezium_evento_artista_slot",
      "publication.name": "debezium_evento_artista_pub",
      "tombstones.on.delete": "false",
      "decimal.handling.mode": "double"
    }
  }'

echo "\n3. Cadastrando Conector Elasticsearch..."
curl -s -X POST http://connect:8083/connectors \
  -H "Content-Type: application/json" \
  -d '{
    "name": "elasticsearch-sink-evento-artista",
    "config": {
      "connector.class": "io.confluent.connect.elasticsearch.ElasticsearchSinkConnector",
      "tasks.max": "1",
      "topics": "ticketeira.public.evento_artista",
      "connection.url": "http://elasticsearch:9200",
      "connection.username": "elastic",
      "connection.password": "pF*vitEA0z*xZ-ENF91a",
      "key.ignore": "false",
      "schema.ignore": "true",
      "transforms": "unwrap",
      "transforms.unwrap.type": "io.debezium.transforms.ExtractNewRecordState",
      "transforms.unwrap.drop.tombstones": "false"
    }
  }'

echo "Concluido!"