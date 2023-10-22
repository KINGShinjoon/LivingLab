#3가지 라이브러리를 가져옵니다.
import cv2
import mediapipe as mp
import numpy as np
from selenium import webdriver
from selenium.webdriver.common.keys import Keys




#감지할 손의 개수와 각 제스쳐의 대한 레이블 정합니다.
max_num_hands = 2
gesture = {
    0: '0', 1: '1', 2: '2', 3:'3', 4:'4', 5:'5'
    ,6: '6', 7:'7' , 8:'8', 9:'9',10:'10',
}
 #Mediapipe에서 사용할 Hands 모듈과 그리기 유틸리티를 가져오는거에요.
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

# 손 모양을 감지하고 추적하기 위한 Hands 모듈을 초기화합니다.
hands = mp_hands.Hands(
    max_num_hands=max_num_hands,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5)

#CSV 파일로 부터 데이터를 읽어오아 특정 데이터와 레이블 데이터로 분리합니다.
file = np.genfromtxt('Data/gesture_train.csv', delimiter=',')
angle = file[:,:-1].astype(np.float32)
label = file[:,-1].astype(np.float32)

#K-nearest Neighbors(KNN) 분류기를 초기화 하고 훈련합니다.
knn= cv2.ml.KNearest_create()
knn.train(angle, cv2.ml.ROW_SAMPLE, label)

# 비디오 캡쳐를 활성화 시킴
cap = cv2.VideoCapture(0)

#노트북 혹은 컴퓨터 캠이 활성화 되어있을 때
while cap.isOpened():
    ret, img = cap.read()
    if not ret:
        continue

    #캠을 뒤집고
    img = cv2.flip(img, 1)
    #손 모양을 찾기 위해서 BGR 2 RGB
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

    #손이 존재하는지 확인용도
    result = hands.process(img)

    #원래 화면으로 가져오기 위해섯 RGB 2 BGR
    img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)

    #result 비어있는게 아닐 경우, 손이 존재하는경우
    if result.multi_hand_landmarks is not None:
        for res in result.multi_hand_landmarks:
            joint = np.zeros((21, 3))
            for j, lm in enumerate(res.landmark):
                joint[j] = [lm.x, lm.y, lm.z]

            # Compute angles between joints
            v1 = joint[[0,1,2,3,0,5,6,7,0,9,10,11,0,13,14,15,0,17,18,19],:] # Parent joint
            v2 = joint[[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20],:] # Child joint
            v = v2 - v1 # [20,3]
            # Normalize v
            v = v / np.linalg.norm(v, axis=1)[:, np.newaxis]

            # Get angle using arcos of dot product
            angle = np.arccos(np.einsum('nt,nt->n',
                v[[0,1,2,4,5,6,8,9,10,12,13,14,16,17,18],:],
                v[[1,2,3,5,6,7,9,10,11,13,14,15,17,18,19],:])) # [15,]

            angle = np.degrees(angle) # Convert radian to degree

            data = np.array([angle], dtype=np.float32)
            ret, results, neighbours, dist = knn.findNearest(data, 3)
            idx = int(results[0][0])

            cv2.putText(img, text=gesture[idx].upper(),
                        org=(int(res.landmark[0].x * img.shape[1]),
                             int (res.landmark[0].y * img.shape[0] + 20)),
                        fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=1,
                        color=(255,255,255),thickness=2)
            mp_drawing.draw_landmarks(img, res, mp_hands.HAND_CONNECTIONS)

            driver = webdriver.Chrome()
            if idx == 1:
                driver.get("https://www.google.com")
            if idx ==0:
                driver.close()

    cv2.imshow('img',img)
    if cv2.waitKey(1) == ord('1'):
        break;
