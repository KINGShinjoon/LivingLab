from flask import Flask, request,jsonify
from flask_cors import CORS

from Util.test import test

app = Flask(__name__)
CORS(app)

app.route('/test',methods=['POST'])(test)

if __name__ == '__main__':
    app.run(host='127.0.0.1',port=5000)