// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $('[name="createAccount"]').on('click', handleCreateAccount);
    $('[name="enviroments"]').on('change', loadContacts);
    $('[name="formContact" ]').on('submit', handleCreateContact);

    loadEnvironments();
});


async function handleCreateAccount() {

    const response = await createAccount();
    if (response.status !== 200) {

        alert('Erro');
        return;
    }

    const data = response.data;

    $('[name="enviroments"]').append(new Option(data.environmentId, data.environmentId, true, true));

    $('[name="empty"]').css('display', 'none');
    $('[name="contactControl"]').css('display', '');

    loadContacts();
}

async function handleCreateContact(e) {

    e.preventDefault();

    const environmentId = $('[name="enviroments"]').val();
    if (!environmentId)
        return;

    const form = $('[name="formContact" ]')[0];

    const contact = {
        name: form.name.value,
        phone: form.phone.value
    };

    await createContact(environmentId, contact);

    const $contactList = $('[name="contactList"]');
    $contactList.append(`<li class="list-group-item"><h4>${contact.name}</h4> <span>${contact.phone}</span></li>`);
}

async function loadEnvironments() {

    const response = await getEnviroments();

    if (response.status !== 200) {

        alert('Erro');
        return;
    }

    const environments = response.data;

    const $selEnvs = $('[name="enviroments"]');
    for (const env of environments) {

        $selEnvs.append(new Option(env, env, true, true));
    }

    if (environments.length > 0) {

        $('[name="empty"]').css('display', 'none');
        $('[name="contactControl"]').css('display', '');
        await loadContacts();
    }
}

async function loadContacts() {

    const environmentId = $('[name="enviroments"]').val();
    if (!environmentId)
        return;

    const $contactList = $('[name="contactList"]');
    $contactList.empty();

    const response = await getContacts(environmentId);
    if (response.status !== 200) {

        alert('Erro');
        return;
    }

    const contacts = response.data;
    
    for (const contact of contacts) {

        $contactList.append(`<li class="list-group-item"><h4>${contact.name}</h4> <span>${contact.phone}</span></li>`);
    }
}

// ========================


async function createAccount() {

    return await fetchJson('POST', '/environments');
}

async function getEnviroments() {

    return await fetchJson('GET', '/environments');
}

async function createContact(environmentId, contact) {

    return await fetchJson('POST', '/contacts?environmentId=' + environmentId, contact);
}

async function getContacts(environmentId) {

    return await fetchJson('GET', '/contacts?environmentId=' + environmentId);
}



async function fetchJson(method, url, reqData) {

    const options = {
        method: method
    };

    if (reqData) {

        options.headers = {
            'Content-Type': 'application/json'
        };

        options.body = JSON.stringify(reqData);
    }

    const response = await fetch(url, options);

    //const contentLength = response.headers.get('Content-Length');
    //if (contentLength === 0)
    //    return { status: response.status };

    const contentType = response.headers.get('Content-Type');
    if (!contentType || contentType.indexOf('application/json') === -1)
        return { status: response.status };

    const data = await response.json();
    return { status: response.status, data };
}