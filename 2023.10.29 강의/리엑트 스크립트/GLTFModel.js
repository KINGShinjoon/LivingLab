import React, { useRef, useEffect } from 'react';
import { useLoader } from '@react-three/fiber';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';

function GLTFModel({ url }) {
  const gltf = useLoader(GLTFLoader, url);
  const ref = useRef();

  useEffect(() => {
    if (ref.current) {
      ref.current.position.set(1, -3, -2);
    }
  }, [ref.current]);

  return <primitive ref={ref} object={gltf.scene} />;
}
export default GLTFLoader;
