import requests
import random

from faker import Faker

faker = Faker()

url = "http://localhost:5044/api/v1/employee"

for value in range(0,10):
    
    data = {
    "Name": faker.name(),
    "Age": random.randint(0, 1000)
    }
    
    headers = {
        'accept': '*/*',
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbXBsb3llZUlkIjoiMCIsIm5iZiI6MTcwNzg2NDU2OSwiZXhwIjoxNzA3OTUwOTY5LCJpYXQiOjE3MDc4NjQ1Njl9.hi7ga5e0KMcYmQ-x5qJ8WtBPuV_zEOYNwr8RquiL2FY',
        # requests won't add a boundary if this header is set when you pass files=
        # 'Content-Type': 'multipart/form-data',
    }
    files = {
        'photo': ('filename.jpg', requests.get(f'https://i.pravatar.cc/400?img={random.randint(0, 70)}').content)
    }

    response = requests.post(url=url, data=data, files=files,headers=headers)

    print("Response:", response.json())
        


