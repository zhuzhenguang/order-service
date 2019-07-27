node{
    stage('Checkout') {
        checkout scm
    }

    stage('Test') {
        sh 'ls -la'
    }

    withCredentials([[$class: 'FileBinding', credentialsId: 'KUBE_CONFIG', variable: 'KUBE_CONFIG']]) {

      withEnv([
        'SERVICE=order-service',
        'PROFILES=dev'
      ]){
          stage('Build') {
              sh './build.sh'
          }

          stage('Deploy') {
              sh './deploy.sh'
          }
      }

    }
}
