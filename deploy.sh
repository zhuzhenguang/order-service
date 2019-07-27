#!/usr/bin/env bash

LOCAL_TAG=cloud-native-$TEAM:$SERVICE-$BUILD_NUMBER
REMOTE_TAG=$ECR_HOST/$LOCAL_TAG
PROFILES=dev
NAMESPACE="$TEAM-$PROFILES"

sed "s#{{profiles}}#$PROFILES#g; s#{{team}}#$TEAM#g; s#{{image}}#$REMOTE_TAG#g; s#{{service}}#$SERVICE#g; s#{{namespace}}#$NAMESPACE#g" kube.yaml

sed "s#{{profiles}}#$PROFILES#g; s#{{team}}#$TEAM#g; s#{{image}}#$REMOTE_TAG#g; s#{{service}}#$SERVICE#g; s#{{namespace}}#$NAMESPACE#g" kube.yaml | sudo kubectl --kubeconfig $KUBE_CONFIG apply -f -
