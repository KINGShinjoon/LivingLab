import React,{useState, useEffecct, useEffect} from 'react';

function Voice(){
    const [transcript, setTranscript] = useState('');
    const [isListening, setIsListening] = useState(false);
    const [recognition, setRecognition] = useState(null);
  
    useEffect(() => {
      const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
      if (SpeechRecognition) {
        const recognitionInstance = new SpeechRecognition();
        recognitionInstance.continuous = true;
        recognitionInstance.interimResults = true;
        recognitionInstance.onresult = (event) => {
          const currentTranscript = Array.from(event.results)
            .map((result) => result[0].transcript)
            .join('');
          setTranscript(currentTranscript);
        };
        setRecognition(recognitionInstance);
      } else {
        console.error('Your browser does not support Speech Recognition API');
      }
    }, []);
  
    const startListening = () => {
      if (recognition) {
        recognition.start();
        setIsListening(true);
      }
    };
  
    const stopListening = () => {
      if (recognition) {
        recognition.stop();
        setIsListening(false);
      }
    };
  
    return (
      <div className="App">
        <header className="App-header">
          <h1>Voice to Text</h1>
          <button onClick={isListening ? stopListening : startListening}>
            {isListening ? 'Stop Listening' : 'Start Listening'}
          </button>
          <p>{transcript}</p>
        </header>
      </div>
    );
  }

export default Voice;