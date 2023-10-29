import time

from flask import Flask, request,jsonify
import openai
openai.api_key = "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"

def test():
    data = request.json
    message = data.get('message','')
    response = openai.ChatCompletion.create(
        model="gpt-3.5-turbo",
        messages=[
            {
                "role": "system",
                "content": "카테고리 : \n인사, 날씨, 메일\n\n내용을 보고 어느 카테고리에 들어가는지 말해줘."
            },
            {
                "role": "user",
                "content": "안녕하세요"
            },
            {
                "role": "assistant",
                "content": "인사"
            },
            {
                "role": "user",
                "content": "오늘 비가 올까?"
            },
            {
                "role": "assistant",
                "content": "날씨"
            },
            {
                "role": "user",
                "content": "반가워"
            },
            {
                "role": "assistant",
                "content": "인사"
            },
            {
                "role": "user",
                "content": "오늘 몇도야?"
            },
            {
                "role": "assistant",
                "content": "날씨"
            },
            {
                "role": "user",
                "content": message
            }
        ],
        temperature=1,
        max_tokens=256,
        top_p=1,
        frequency_penalty=0,
        presence_penalty=0
    )
    response_message = ""
    assistant_message = response['choices'][0]['message']['content']
    if "날씨" in assistant_message:
        from bs4 import BeautifulSoup  # beautifulsoup4
        from pprint import pprint
        import requests
        html = requests.get(
            'https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query=%EB%82%A0%EC%94%A8')
        # print(html.text)

        soup = BeautifulSoup(html.text, 'html.parser')

        data1 = soup.find('section', {'class': 'sc_new cs_weather_new _cs_weather'})
        find_address = data1.find('h2', {'class': 'title'}).text
        print('현재 위치: ' + find_address)
        data2 = soup.find('div', {'class': 'temperature_text'})
        temp_tag = data2.find('strong')
        for span in temp_tag.find_all('span'):
            span.decompose()
        find_currenttmp = temp_tag.get_text(strip=True)
        print('현재 온도: ' + find_currenttmp + '℃')

        response_message = find_address +'의 날씨는' + find_currenttmp + '℃ 입니다.'
    elif "인사" in assistant_message:
        response_message = "인사 관련 내용입니다."
    elif "메일" in assistant_message:
        import time
        from selenium import webdriver
        from selenium.webdriver.common.by import By

        driver = webdriver.Chrome()
        driver.get('https://nid.naver.com/nidlogin.login?mode=form&url=https://www.naver.com/')
        id_input = driver.find_element(By.ID, 'id')
        pw_input = driver.find_element(By.ID, 'pw')

        id_input.send_keys('test')
        pw_input.send_keys('')

        time.sleep(40)
        login_button = driver.find_element(By.CSS_SELECTOR, '.btn_global')

        login_button.click()

        driver.quit()

        response_message = "메일 관련 내용입니다."
    print(assistant_message)
    return jsonify({"Response":response_message})
