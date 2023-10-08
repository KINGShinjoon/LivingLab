from flask import Flask, jsonify, request

app = Flask(__name__)

# 데이터를 저장할 간단한 리스트
data = [
    {"id": 1, "name": "홍길동"},
    {"id": 2, "name": "홍길수"},
    {"id": 3, "name": "홍길지"}
]

# 모든 데이터를 반환하는 엔드포인트
@app.route('/api/data', methods=['GET'])
def get_data():
    return jsonify(data)

# 특정 ID의 데이터를 반환하는 엔드포인트
@app.route('/api/data/<int:data_id>', methods=['GET'])
def get_data_by_id(data_id):
    item = next((item for item in data if item['id'] == data_id), None)
    if item is not None:
        return jsonify(item)
    else:
        return jsonify({"error": "Data not found"}), 404

# 데이터를 생성하는 엔드포인트
@app.route('/api/data', methods=['POST'])
def create_data():
    new_item = request.get_json()
    if 'id' in new_item and 'name' in new_item:
        data.append(new_item)
        return jsonify({"message": "Data created successfully"}), 201
    else:
        return jsonify({"error": "Invalid data format"}), 400

if __name__ == '__main__':
    app.run(debug=True)
