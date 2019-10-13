# Node image can be used as base image for angular app
FROM node:latest

# Set working directory
WORKDIR /app

# Add node modules bins to $PATH
ENV PATH /app/src/WebSpa/node_modules/.bin:$PATH

# Install and cache app dependencies
COPY src/WebSpa/package.json /app/src/WebSpa/package.json

WORKDIR /app/src/WebSpa
RUN npm install
RUN npm install -g @angular/cli@latest

# Add the app
COPY . /app

# Start the app
CMD cd /app/src/WebSpa && ng serve --host 0.0.0.0