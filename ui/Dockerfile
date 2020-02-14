FROM node:12.2.0-alpine

ENV PATH /app/node_modules/.bin:$PATH

WORKDIR /app

COPY package.json /app/package.json
RUN npm install
RUN npm install @vue/cli@4.2.2 -g
