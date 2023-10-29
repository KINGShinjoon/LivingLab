// import React, { useRef, useEffect } from 'react';
// import { useLoader } from '@react-three/fiber';
// import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';

// function GLTFModel({ url }) {
//   const gltf = useLoader(GLTFLoader, url);
//   const ref = useRef();

//   useEffect(() => {
//     if (ref.current) {
//       // 스케일 설정
//       ref.current.scale.set(0.1, 0.1, 0.1);
//       ref.current.rotation.y = Math.PI;
//       // 위치 설정
//       ref.current.position.set(1, -3, -2);
//     }
//   }, [ref.current]);

//   return <primitive ref={ref} object={gltf.scene} />;
// }

// export default GLTFModel;

import React, { useRef, useEffect, useMemo } from 'react';
import { useLoader, useFrame } from '@react-three/fiber';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { AnimationMixer, Vector3, Euler } from 'three';

function GLTFModel({ url }) {
  const gltf = useLoader(GLTFLoader, url);
  const ref = useRef();
  const mixer = useMemo(() => new AnimationMixer(), []); // AnimationMixer 인스턴스 생성

  useEffect(() => {
    if (ref.current) {
      // 모델의 위치, 회전, 스케일 설정
      ref.current.position.set(1, -3, -2);
      ref.current.rotation.y = Math.PI;
      ref.current.scale.set(0.1, 0.1, 0.1);

    }
    if (ref.current && gltf.animations.length > 0) {
        // 특정 이름을 가진 애니메이션 찾기
        const animation = gltf.animations.find(clip => clip.name === "Idle 01");
  
        // 애니메이션 실행
        if (animation) {
          mixer.clipAction(animation, ref.current).play();
        } else {
        }
      }
  }, [ref.current, gltf.animations, mixer]);

  useFrame((state, delta) => mixer.update(delta)); // 매 프레임마다 mixer를 업데이트

  return <primitive ref={ref} object={gltf.scene} />;
}

export default GLTFModel;
