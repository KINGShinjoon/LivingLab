# LivingLab
리빙랩 관련 강의 자료

유니티 주소
- https://unity.com/kr

유니티 허브 다운
- https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe

유니티 엔진 다운
- https://unity.com/releases/editor/archive

아나콘다 설치
- https://www.anaconda.com/download

#아나콘다 가상 환경 생성
conda create -n <환경명> python=<버전(ex:3.5이나 3.7 등)>
conda create -n koreait python=3.7

#아나콘다 목록 확인
conda env list

#아나콘다 활성화
conda activate 환경명

유니티 ML
- https://github.com/Unity-Technologies/ml-agents

유니티 ML 18 Docs
- https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Readme.md

훈련 
mlagents-learn config/ppo/3DBall.yaml --run-id=first3DBallRun
