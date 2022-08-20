import { useEffect, useState } from "react";


function InputPage() {
    const [firstLoad, setFirstLoad] = useState(
        true
    )

    const [id, setId] = useState(
        0
    )

    const [error, setError] = useState({
        isError: false,
        errorMessage: ""
    })

    function handleKeyPress(event: React.KeyboardEvent): void {
        if (event.key === "Enter") {
            generateUrl();
        }
    }

    useEffect(() => {
    // Prevent useEffect firing when page first loads, so result is not automatically shown
    if (!firstLoad) {
        toggleErrorOrResult();
    }
    setFirstLoad(false);
    }, [id, error])

    function generateUrl(): void {
    var input = document.getElementById("url-input") as HTMLInputElement;
    var urlString: string = input.value;
    var apiUrl: string = process.env.REACT_APP_API_URL as string;
    
    // Check if url is in valid format before calling backend
    if (isValidUrl(urlString)) {
        fetch(apiUrl, {
            method: 'POST',
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            },
            body: JSON.stringify({
            "Url" : urlString
            })
            })
        .then(res => res.json())
        .then(res => {
            if (res.id) {
                setId(res.id);
                setError({
                isError: false,
                errorMessage: ""
                });
            } else if (res.Error) {
                setError({
                isError: true,
                errorMessage: res.Error.errors[0].errorMessage
                });
            }
        }).catch(res => {
            setError({
                isError: true,
                errorMessage: "Sorry, something went wrong with your request. " +
                    "Please try again later or contact site administrator for help."
            });
        });
        } else {
            setError({
            isError: true,
            errorMessage: "Please enter a valid url."
            });
        }
    };

    function isValidUrl(input: string): boolean {
        // Credit to https://gist.github.com/dperini/729294 for this RegEx
        var weburl = new RegExp(
            "^(?:(?:(?:https?|ftp):)?\\/\\/)(?:\\S+(?::\\S*)?@)?(?:(?!(?:10|127)(?:\\.\\d{1,3}){3})(?!(?:169\\.254|192\\.168)(?:\\.\\d{1,3}){2})](?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))|(?:(?:[a-z0-9\\u00a1-\\uffff][a-z0-9\\u00a1-\\uffff_-]{0,62})?[a-z0-9\\u00a1-\\uffff]\\.)+(?:[a-z\\u00a1-\\uffff]{2,}\\.?))(?::\\d{2,5})?(?:[/?#]\\S*)?$", "i"
        );
        
        if (!weburl.test(input)) {
            return false;
        }

        return true;
    }

    function toggleErrorOrResult(): void {
        var result = document.getElementById("result") as HTMLElement;
        var errorElement = document.getElementById("error") as HTMLElement;
        
        if (error.isError === true) {
            hide(result);
            show(errorElement);
        } else {
            hide(errorElement);
            show(result);
        }
    }

    function hide(element: HTMLElement): void {
        element.classList.add("hide");
    };

    function show(element: HTMLElement): void {
        element.classList.remove("hide");
    };

    return (
        <div>
            <p>Enter a long url, and have it returned to you as a shortened url which points to the same place.</p>
            <div className="input-section">
                <input onKeyDown={(e) => handleKeyPress(e)} id="url-input" type="text" placeholder="Enter URL here..."></input>
                <button onClick={() => generateUrl()}>Generate MagicURL</button>
            </div>
            <p className="hide" id="result">Your magic url is {window.location.toString() + id.toString()} </p>
            <p className="hide" id="error">{error.errorMessage}</p>
        </div>
    )
}

export default InputPage;