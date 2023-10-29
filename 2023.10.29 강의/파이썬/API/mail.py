import time
from selenium import webdriver
from selenium.webdriver.common.by import By

driver = webdriver.Chrome()
driver.get('https://nid.naver.com/nidlogin.login?mode=form&url=https://www.naver.com/')
id_input = driver.find_element(By.ID, 'id')
pw_input = driver.find_element(By.ID, 'pw')

id_input.send_keys('test')
pw_input.send_keys('')

time.sleep(40)
login_button = driver.find_element(By.CSS_SELECTOR, '.btn_global')

login_button.click()

driver.quit()