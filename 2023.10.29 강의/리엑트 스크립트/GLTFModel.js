import React, { useRef, useEffect,useMemo } from 'react';
import { useLoader,useFrame } from '@react-three/fiber';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import {AnimationMixer} from 'three';

function GLTFModel({ url }) {
  const gltf = useLoader(GLTFLoader, url);
  const ref = useRef();
  const mixer = useMemo(() => new AnimationMixer(),[]);
  useEffect(() => {
    if (ref.current) {
        ref.current.scale.set(0.1,0.1,0.1);
        ref.current.rotation.y = Math.PI;
      ref.current.position.set(1, -3, -2);
    }
    if(ref.current && gltf.animations.length>0){
        const animation = gltf.animations.find 
        (clip => clip.name === "Idle 01");
        if(animation){
            mixer.clipAction(animation,ref.current).play();
        }else{
            console.warn("Animation not found");
        }
    }
  }, [ref.current, gltf.animations, mixer]);

  useFrame((state, delta) => mixer.update(delta));
  return <primitive ref={ref} object={gltf.scene} />;
}
export default GLTFModel;
