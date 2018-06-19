
import { Promise } from 'es6-promise';

export const webAPIService = {
    get,
    put,
    post,
    delete: _delete,
    handleResponse,
    handleError
};

const apiUrl = '/api/';

function get(url: string, params?: URLSearchParams) {
    const requestOptions = {
        method: 'GET',
        headers: {
            accept: 'application/json'
        }
    };

    if (params != null && params !== undefined) {
        url += "?" + params.toString();
    }

    return fetch(apiUrl + url, requestOptions).then(handleResponse, handleError);
}
function put(url: string, entity: any) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'accept': 'application/json'
        },
        body: JSON.stringify(entity)
    };

    return fetch(apiUrl + url, requestOptions).then(handleResponse, handleError);
}
function _delete(url: string) {
    const requestOptions = {
        method: 'DELETE'
    };

    return fetch(apiUrl + url, requestOptions).then(handleResponse, handleError);
}
function post(url: string, entity: any) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'accept': 'application/json'
        },
        body: JSON.stringify(entity)
    };

    return fetch(apiUrl + url, requestOptions).then(handleResponse, handleError);
}
function handleResponse(response: any) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                response.json().then((json: any) => {
                    resolve(json);
                });
            } else {
                resolve();
            }
        } else {
           
            response.text().then((text: any) => reject(text));
        }
    });
}
function handleError(error: any) {
    return Promise.reject(error && error.message);
}
