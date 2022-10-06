terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.16"
    }
  }

  required_version = ">= 1.2.0"
}

provider "aws" {
  region = "ap-southeast-2"
}

locals {
  repository = "fitness-tracker"
  version    = "0.0.1"
  region     = "ap-southeast-2"
  tags = {
    Name        = "fitness-tracker"
    Owner       = "root"
    Environment = "prod"
  }
}

variable "CONNECTION_STRING" {
  type        = string
  description = "The DB connection string."
  sensitive = true
}

resource "aws_security_group" "sg" {
  name        = "${local.repository}"
  description = "${local.repository}"
  
  ingress {
    description      = "Total access"
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }
}

resource "aws_s3_bucket" "bucket" {
  bucket = "${local.repository}-kfc-bucket"
  tags   = local.tags
}

resource "aws_instance" "app_server" {
  ami = "ami-0c641f2290e9cd048"
  instance_type = "t2.micro"
  iam_instance_profile = "ec2-docker"
  vpc_security_group_ids = [ aws_security_group.sg.id ]
  user_data = <<-EOT
                #!/bin/sh

                yum update -y
                amazon-linux-extras install docker
                service docker start
                usermod -a -G docker ec2-user
                chkconfig docker on

                ACCOUNT_ID=`aws sts get-caller-identity --query "Account" --output text`
                aws ecr get-login-password --region ${local.region} | docker login --username AWS --password-stdin $ACCOUNT_ID.dkr.ecr.${local.region}.amazonaws.com

                docker pull $ACCOUNT_ID.dkr.ecr.${local.region}.amazonaws.com/${local.repository}:latest
                docker run -e CONNECTION_STRING="${var.CONNECTION_STRING}" -e BUCKET="${aws_s3_bucket.bucket.bucket_domain_name}" -p 80:80 $ACCOUNT_ID.dkr.ecr.${local.region}.amazonaws.com/${local.repository}:latest
                EOT
  tags = local.tags
}

resource "aws_eip" "elastic_ip" {
  instance = aws_instance.app_server.id
}

output "server_ip" {
  value = aws_eip.elastic_ip.public_ip
}
