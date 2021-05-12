# MockBilling
 
NGINX Settings 

    proxy_buffer_size   128k;
    proxy_buffers   4 256k;
    proxy_busy_buffers_size   256k;
    large_client_header_buffers 4 16k;

    location / {
        proxy_pass http://localhost:5000; #https
        proxy_http_version 1.1;           #http
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection "upgrade";
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;#mail

        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;

        add_header Access-Control-Allow-Origin *;
        add_header 'Access-Control-Allow-Methods' 'GET, POST, OPTIONS';

        fastcgi_buffers 16 16k;
        fastcgi_buffer_size 32k;
    }


Set an app port
~/appsettings.json

     "Urls": "http://localhost:5005",
