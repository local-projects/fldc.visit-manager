# sm, 21.01.2020

import requests
import time
import json

headers = {'content-type': 'application/json'}
clientUrl = "http://192.168.20.180:8080/"  # trailing '/' is mandatory

# write SYS_ID
payload = {
          'parameter': 'SYS_ID',
          'value': 'CP0002',
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "writeConfig", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)


# write PORT_LISTEN
payload = {
          'parameter': 'PORT_LISTEN',
          'value': '8080',
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "writeConfig", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

# write URL_SERVER
payload = {
          'parameter': 'URL_SERVER',
          'value': 'http://192.168.20.108:8080',
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "writeConfig", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

# write HELLO_MSG_INVERVAL
payload = {
          'parameter': 'HELLO_MSG_INVERVAL',
          'value': '5',
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "writeConfig", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

# write SYSTEM_TYPE
payload = {
          'parameter': 'SYSTEM_TYPE',
          'value': 'CP_LED',
#          'value': 'CP_CASH',
#          'value': 'IRCHG_CHAGER',
#          'value': 'IRCHG_BLASTER',
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "writeConfig", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(10)
