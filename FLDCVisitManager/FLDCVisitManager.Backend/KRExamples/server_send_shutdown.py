# sm, 21.01.2020

import requests
import time
import json

headers = {'content-type': 'application/json'}
clientUrl = "http://192.168.20.180:8080/"  # trailing '/' is mandatory

# send shutdown
payload = {
          'reboot': False,
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "shutdown", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(1)

