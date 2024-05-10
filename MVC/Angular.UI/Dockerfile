### STAGE 1:BUILD ###
FROM node:latest AS build
WORKDIR /dist/src/app
COPY . .
RUN npm cache clean --force
RUN npm install
RUN npm run build

### STAGE 2:RUN ###
FROM nginx:latest AS ngi
COPY --from=build /dist/src/app/dist/angular.ui /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80