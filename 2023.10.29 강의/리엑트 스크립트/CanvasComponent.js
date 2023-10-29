import React, { useState } from "react";
import axios from "axios";

import { Canvas } from "@react-three/fiber";
import GLTFModel from "./GLTFModel";
import "./CanvasComponent.css";
function CanvasComponent() {
  const [inputValue, setInputValue] = useState("");
  const [response, setResponse] = useState("");
  const [loading, setLoading] = useState(false);
  const handleInputChange = (event) => {
    setInputValue(event.target.value);
  };

  const handleSubmit = async () => {
    setLoading(true); // 요청 시작 시 로딩 상태를 true로 설정
    try {
      const result = await axios.post('http://127.0.0.1:5000/hello', { message: inputValue });
      setResponse(result.data.response);
    } catch (error) {
      console.error('API 요청 중 오류 발생:', error);
      setResponse('오류가 발생했습니다.');
    }
    setLoading(false); // 요청 완료 시 로딩 상태를 false로 설정
  };
  
  return (
    <div className="canvasStyle">
      <Canvas>
        <ambientLight />
        <pointLight position={[0, 0, 0]} />
        <GLTFModel url={process.env.PUBLIC_URL + "/human.gltf"} />
      </Canvas>
      <div>
      <input type="text" value={inputValue} onChange={handleInputChange} />
      <button onClick={handleSubmit} disabled={loading}>전송</button>
      <div>
        {loading ? <p>로딩 중...</p> : <p><strong>응답:</strong> {response}</p>}
      </div>
    </div>
    </div>
  );
}

export default CanvasComponent;
