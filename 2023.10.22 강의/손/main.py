import cv2 #opencsv-python
import mediapipe as mp
import numpy as np

max_num_hands = 1
gesture = {
    0: '0', 1: '1', 2: '2', 3:'3', 4:'4', 5:'5'
    ,6: '6', 7:'7' , 8:'8', 9:'9',10:'10',
}

mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

hands = mp_hands.Hands(
    max_num_hands=max_num_hands,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5)

file = np.genfromtxt('Data/gesture_train.csv', delimiter=',')
angle = file[:,:-1].astype(np.float32)
label = file[:,-1].astype(np.float32)
knn= cv2.ml.KNearest_create()
knn.train(angle, cv2.ml.ROW_SAMPLE, label)

image_path = 'data/1.jpg'
image = cv2.imread(image_path)

cv2.imshow('Image',image)
cv2.waitKey(0)
cv2.destroyAllWindows()
