# sm, 21.01.2020

import requests
import time
import json

headers = {'content-type': 'application/json'}
clientUrl = "http://192.168.20.180:8080/"  # trailing '/' is mandatory

# send fwUpdate
payload = {
#          'url': "ftp://simon.maurer:tds-5034B@192.168.20.19/download_test.txt",
          'ftpServer': "192.168.20.19",
          'ftpPath': "/",
          'ftpFile': "abs_fw.tar.bz2",
          'ftpUser': "simon.maurer",
          'ftpPw': "password",
          }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "fwUpdate", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(1)

