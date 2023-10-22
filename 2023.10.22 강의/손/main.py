#3가지 라이브러리를 가져옵니다.
import cv2
import mediapipe as mp
import numpy as np

#감지할 손의 개수와 각 제스쳐의 대한 레이블 정합니다.
max_num_hands = 1
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

#이미지를 불러오고 읽어와서 화면에 표시하빋나.
image_path = 'data/1.jpg'
image = cv2.imread(image_path)
image = cv2.cvtColor(image,cv2.COLOR_BGR2RGB)

result = hands.process(image)

image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

if result.multi_hand_landmarks is not None:
    for res in result.multi_hand_landmarks:
        joint = np.zeros((21, 3))
        for j, lm in enumerate(res.landmark):
            joint[j] = [lm.x, lm.y, lm.z]

        v1 = joint[[0,1,2,3,0,5,6,7,0,9,10,11,0,13,14,15,0,17,18,19],:]
        v2 = joint[[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20],:]

        v = v2-v1
        v = v / np.linalg.norm(v, axis=1)[:, np.newaxis]

        angle = np.arccos(np.einsum('nt,nt->n',
                                    v[[0, 1, 2, 4, 5, 6, 8, 9, 10, 12, 13, 14, 16, 17, 18], :],
                                    v[[1, 2, 3, 5, 6, 7, 9, 10, 11, 13, 14, 15, 17, 18, 19], :]))
        angle = np.degrees(angle)

        data=np.array([angle], dtype=np.float32)
        ret, results, neighbours, dist = knn.findNearest(data,3)
        idx=int(results[0][0])

        if idx in gesture.keys():
            cv2.putText(image, text=gesture[idx].upper(),
                        org=(int(res.landmark[0].x*image.shape[1]),
                             int(res.landmark[0].y * image.shape[0] + 20)),
                        fontFace=cv2.FONT_HERSHEY_SIMPLEX,
                        fontScale=1, color=(255,255,255),
                        thickness=2)
    mp_drawing.draw_landmarks(image,res,mp_hands.HAND_CONNECTIONS)

cv2.namedWindow('image',cv2.WINDOW_NORMAL)
cv2.resizeWindow('image',600,900)

cv2.imshow('image',image)
cv2.waitKey(0)
cv2.destroyAllWindows()