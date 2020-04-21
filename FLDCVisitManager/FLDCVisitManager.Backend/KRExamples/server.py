#!/usr/bin/env python3
"""
Very simple HTTP server in python for logging requests
Usage::
    ./server.py [<port>]
"""
from http.server import BaseHTTPRequestHandler, HTTPServer
import logging
import json
import requests

ledCounter = 0

class Server(BaseHTTPRequestHandler):
    def _set_response(self):
        self.send_response(200)
        self.send_header('Content-type', 'application/json')
        self.end_headers()

#    def do_GET(self):
#        logging.info("GET request,\nPath: %s\nHeaders:\n%s\n", str(self.path), str(self.headers))
#        self._set_response()
#        self.wfile.write("GET request for {}".format(self.path).encode('utf-8'))

    def do_POST(self):
        content_length = int(self.headers['Content-Length']) # <--- Gets the size of data
        post_data = self.rfile.read(content_length) # <--- Gets the data itself
        logging.info("POST request,\nPath: %s\nHeaders:\n%s\n\nBody:\n%s\n",
                str(self.path), str(self.headers), post_data.decode('utf-8'))

        self._set_response()
        outDict = dict()
        outDict["status"] = "OK"
        
        self.wfile.write(json.dumps(outDict).encode('utf-8'))
        
        ## Send response to CP
        #######################
        # decode JSON
        path = str(self.path.replace('/', '',1)) # remove 1st '/' in path
        dictIn = json.loads(post_data.decode('utf-8'))
        global ledCounter
        self.ledColors = [
            {'color': 0xFF0000 },
            {'color': 0x00FF00 },
            {'color': 0x0000FF },
            {'color': 0x808000 },
            {'color': 0x008080 },
            {'color': 0x800080 },
        ]            
        if path == "cpLamp":
            headers = {'content-type': 'application/json'}
            clientUrl = "http://192.168.20.180:8080/"  # trailing '/' is mandatory
            payload = self.ledColors[ledCounter]
            print("Data: " + json.dumps(payload))
            r = requests.post(clientUrl + "setLedColor", data=json.dumps(payload), headers=headers)
            print("Status: " + str(r.status_code))
            print("Body: " + r.text)
            
            ledCounter += 1
            if ledCounter >= len(self.ledColors):
                ledCounter = 0
        
        
def run(server_class=HTTPServer, handler_class=Server, port=8080):
    logging.basicConfig(level=logging.INFO)
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    logging.info('Starting httpd...\n')
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        pass
    httpd.server_close()
    logging.info('Stopping httpd...\n')

if __name__ == '__main__':
    from sys import argv

    if len(argv) == 2:
        run(port=int(argv[1]))
    else:
        run()
