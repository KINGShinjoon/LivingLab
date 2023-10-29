import React,{useState} from "react";
import { Canvas }from "@react-three/fiber";
import GLTFModel from "./GLTFModel";
import "./CanvasComponent.css";
import axios from "axios";

function CanvasComponent(){
    const [loading, setLoading] = useState(false);
    const [Response, setResponse] = useState("");
    const [input, setInput] = useState("");

    const handleInputChange =(event) =>{
        setInput(event.target.value);
    }
    
    //서버 연결
    const handletestSubmit = async () =>{
        setLoading(true); //로딩중 이라는 메세지 표기
        try{
            const result = await axios.post('http://127.0.0.1:5000/test'
            ,{message:input});
            setResponse(result.data.Response);
        }
        catch(error){
            console.error('API 요청 중 오류 발생:', error);
            setResponse("오류 발생");
        
        }
        setLoading(false);
    };

    return(
        <div className="canvasStyle">
        <Canvas>
            <ambientLight/>
            <pointLight position={[0, 0, 0]} />
            <GLTFModel url={process.env.PUBLIC_URL + "/human.gltf"} />
        </Canvas>

        <div>
            <input type="text" value={input} onChange={handleInputChange}></input>
            <button type="button" onClick={handletestSubmit} disabled={loading}
            >전송</button>
            <div>
                {loading ? <p>로딩 중...</p> : 
                <p><strong>응답:</strong> {Response}</p>}
            </div>
        </div>
        </div>
    )
}
export default CanvasComponent;