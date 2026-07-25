### Removing the current compose stack and running it again:
echo "Removing and running compose script... "
docker compose down -v # Stop and remove the current compose stack (if present) and clear the volumes (WARNING: '-v' means the data in the volumes will be deleted!).
docker compose build # Build the compose script (if needed).
docker compose up -d # Run the compose script ('-d' means, 'dont show me all of the containers logs and occupy the terminal')
echo "...running!"

### Clustering RabbitMQ instances:
echo "Clustering RabbitMQ nodes..."
sleep 4s
docker exec rabbitmq-b rabbitmqctl stop_app # Stop our secondary node (node 'b').
docker exec rabbitmq-b rabbitmqctl reset # Make sure no other cluster configurations are left.
docker exec rabbitmq-b rabbitmqctl join_cluster mes-shopfloor@rabbitmq-a # Add our secondary node to the cluster of our primary node (node 'a').
docker exec rabbitmq-b rabbitmqctl start_app # Start our secondary node again.
docker exec rabbitmq-b rabbitmqctl cluster_status # Print the status of our secondary node, so we can confirm that both primary and secondary nodes are clustered.
echo "...clustered!"