# Development

## Connecting to the dev database within AWS

To connect to the dev database, you should ensure that you have your AWS CLI set up correctly, and authenticated with AWS.

You'll then use the Session Manager to connect to a bastion within the VPC, from there you'll be able to use `psql` or other to connect to the database.

1. Use SSM to connect to the bastion:
  ```bash
  # Ensure aws is configured for the Development APIs account
  aws ssm start-session --target "$(aws ssm get-parameter --name platform-apis-jump-box-instance-name --query Parameter.Value --output text)"
  ```
2. Set up the env vars to use for psql
  ```bash
  export PGHOST=$(aws ssm get-parameter --name /cv-19-i-need-help-v3/development/postgres-hostname --query Parameter.Value --region eu-west-2 --output text)
  export PGPORT=$(aws ssm get-parameter --name /cv-19-i-need-help-v3/development/postgres-port --query Parameter.Value --region eu-west-2 --output text)
  export PGUSER=$(aws ssm get-parameter --name /cv-19-i-need-help-v3/development/postgres-username --query Parameter.Value --region eu-west-2 --output text)
  export PGPASSWORD=$(aws ssm get-parameter --name /cv-19-i-need-help-v3/development/postgres-password --query Parameter.Value --region eu-west-2 --output text)
  export PGDATABASE=$(aws ssm get-parameter --name /cv-19-i-need-help-v3/development/postgres-database --query Parameter.Value --region eu-west-2 --output text)
  ```
3. Run `psql`
