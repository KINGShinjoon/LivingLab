import React,{useState} from "react";
import { Canvas }from "@react-three/fiber";
import GLTFModel from "./GLTFModel";
import "./CanvasComponent.css";

function CanvasComponent(){
    return(
        <div className="canvasStyle">
        <Canvas>
            <ambientLight/>
            <pointLight position={[0, 0, 0]} />
            <GLTFModel url={process.env.PUBLIC_URL + "/human.gltf"} />
        </Canvas>
        </div>
    )
}
export default CanvasComponent;