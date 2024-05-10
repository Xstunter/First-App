#Docker dont work

### SETUP

### Hosts file

Update hosts file on your PC like in this [instruction](https://www.nublue.co.uk/guides/edit-hosts-file/#:~:text=In%20Windows%2010%20the%20hosts,%5CDrivers%5Cetc%5Chosts.)

Need to path these lines

    127.0.0.1 www.redencywebsite.com
    0.0.0.0 www.redencywebsite.com
    <your-local-ip> (for example 192.168.0.4) www.redencywebsite.com

### Docker

`docker-compose build --no-cache`

`docker-compose up`

