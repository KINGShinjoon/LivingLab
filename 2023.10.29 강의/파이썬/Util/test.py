from flask import Flask, request,jsonify
import openai
openai.api_key = ""

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

    assistant_message = response['choices'][0]['message']['content']
    print(assistant_message)
    return jsonify({"Response":"작업완료"})
