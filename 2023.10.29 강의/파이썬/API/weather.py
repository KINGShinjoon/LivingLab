from bs4 import BeautifulSoup #beautifulsoup4
from pprint import pprint
import requests

html = requests.get('https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query=%EB%82%A0%EC%94%A8')
#print(html.text)

soup = BeautifulSoup(html.text,'html.parser')

data1 = soup.find('section', {'class': 'sc_new cs_weather_new _cs_weather'})
find_address =data1.find('h2',{'class':'title'}).text
print('현재 위치: ' + find_address)
data2 = soup.find('div', {'class': 'temperature_text'})
temp_tag = data2.find('strong')
for span in temp_tag.find_all('span'):
    span.decompose()
find_currenttmp = temp_tag.get_text(strip=True)
print('현재 온도: ' +find_currenttmp + '℃')