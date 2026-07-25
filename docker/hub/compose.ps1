### Removing the current compose stack and running it again:
Write-Host "Removing and running compose script...`n"
docker compose down -v # Stop and remove the current compose stack (if present) and clear the volumes (WARNING: '-v' means the data in the volumes will be deleted!).
docker compose build # Build the compose script (if needed).
docker compose up -d # Run the compose script ('-d' means, 'dont show me all of the containers logs and occupy the terminal')
Write-Host "`n...running!"

### Clustering RabbitMQ instances:
Write-Host "`nClustering RabbitMQ nodes...`n"
Start-Sleep -Seconds 4 # Wait a moment because we need both RabbitMQ instances to actually run. (If problems related to clustering or failing to contact some RabbitMQ instance arise, try increasing the amounts of seconds)
docker exec rabbitmq-b rabbitmqctl stop_app # Stop our secondary node (node 'b').
docker exec rabbitmq-b rabbitmqctl reset # Make sure no other cluster configurations are left.
docker exec rabbitmq-b rabbitmqctl join_cluster mes-shopfloor@rabbitmq-a # Add our secondary node to the cluster of our primary node (node 'a').
docker exec rabbitmq-b rabbitmqctl start_app # Start our secondary node again.
docker exec rabbitmq-b rabbitmqctl cluster_status # Print the status of our secondary node, so we can confirm that both primary and secondary nodes are clustered.
Write-Host "`n...clustered!"