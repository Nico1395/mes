### Removing the current compose stack and running it again:
Write-Host "Removing and running compose script...`n"
docker compose down -v # Stop and remove the current compose stack (if present) and clear the volumes (WARNING: '-v' means the data in the volumes will be deleted!).
docker compose build # Build the compose script (if needed).
docker compose up -d # Run the compose script ('-d' means, 'dont show me all of the containers logs and occupy the terminal')
Write-Host "`n...running!"

### Clustering RabbitMQ instances:
$node_a="rabbitmq-a"
$node_b="rabbitmq-b"

Write-Host "`nClustering RabbitMQ nodes...`n"
docker exec $node_b rabbitmqctl stop_app # Stop our secondary node (node 'b').
docker exec $node_b rabbitmqctl join_cluster mes-shopfloor@$node_a # Add our secondary node to the cluster of our primary node (node 'a').
docker exec $node_b rabbitmqctl start_app # Start our secondary node again.
docker exec $node_b rabbitmqctl cluster_status # Print the status of our secondary node, so we can confirm that both primary and secondary nodes are clustered.
Write-Host "`n...clustered!"