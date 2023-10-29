from flask import Flask, request,jsonify
from flask_cors import CORS

def test():
    data = request.json
    message = data.get('message','')
    response_message = f"{message}"
    return jsonify({"Response":response_message})
