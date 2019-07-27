#!/usr/bin/env bash

set -ex

LOCAL_TAG=cloud-native-$TEAM:$SERVICE-$BUILD_NUMBER
REMOTE_TAG=$ECR_HOST/$LOCAL_TAG

username=$(cat /etc/docker-registry/username)
password=$(cat /etc/docker-registry/password)
endpoint=$(cat /etc/docker-registry/endpoint)

sudo docker login -u $username -p $password $endpoint

TAG=$SERVICE-$BRANCH_NAME-$BUILD_NUMBER

sudo docker build -t $LOCAL_TAG -f Dockerfile.aws .
sudo docker tag $LOCAL_TAG $REMOTE_TAG
sudo docker push $REMOTE_TAG
