document.addEventListener("DOMContentLoaded", function () {
    // Добавляем стили
    const style = document.createElement('style');
    style.textContent = `
        .cookie-notification {
            position: fixed;
            background-color: rgba(0, 0, 0, .8);
            bottom: 0;
            width: 100%;
            color: white;
            padding: 15px;
            z-index: 99999999;
        }

        .cookie-notification_hidden_yes {
            display: none;
        }

        a.c1:link {
            color: yellow;
        }

        a.c1:visited {
            color: white	;
            text-decoration: underline dotted rgba(245,176,65,0.5);
            transition: all 0.2s;
            position: relative;
        }

        a.c1:visited:hover {
            text-decoration-color: #F5B041;
            transform: translateY(-2px);
        }

        a.c1:visited:hover::before {
            opacity: 1;
        }
    `;
    document.head.appendChild(style);



    // Остальной код вашего скрипта
    let agreementCookieSet = false;

    function createNotificationElement() {
        const notificationContainer = document.createElement('div');
        notificationContainer.className = 'cookie-notification';

        const body = document.createElement('div');
        body.className = 'cookie-notification__body';
        body.innerHTML = '<p style="color: white;font-size: 14px;">Продолжая использовать сайт, Вы принимаете условия <a href="/pdfs/politika_pdn.pdf" target="_blank" rel="noopener" class="c1">Политики СВФУ в отношении обработки персональных данных</a> и даёте согласие на обработку пользовательских данных (файлов cookie)</p>';

        const buttons = document.createElement('div');
        buttons.className = 'cookie-notification__buttons';

        const agreeButton = document.createElement('button');
        agreeButton.id = 'yes';
        agreeButton.className = 'btn btn-primary';
        agreeButton.innerHTML = 'Я согласен';
        agreeButton.style.display = 'inline-block';
        agreeButton.style.padding = '10px 20px';
        agreeButton.style.fontSize = '14px';
        agreeButton.style.fontWeight = 'bold';
        agreeButton.style.textAlign = 'center';
        agreeButton.style.textDecoration = 'none';
        agreeButton.style.border = 'none';
        agreeButton.style.borderRadius = '5px';
        agreeButton.style.cursor = 'pointer';
        agreeButton.style.transition = 'background-color 0.3s ease, transform 0.2s ease';
        agreeButton.style.backgroundColor = '#007BFF'; /* Синий цвет */
        agreeButton.style.color = 'white';

        buttons.appendChild(agreeButton);
        notificationContainer.appendChild(body);
        notificationContainer.appendChild(buttons);

        return notificationContainer;
    }

    function setAgreeCookie() {
        const date = new Date();
        date.setFullYear(date.getFullYear() + 1);
        document.cookie = `agreement=1; path=/; expires=${date.toUTCString()}`;
    }

    function checkAgreeCookie() {
        return document.cookie
            .split('; ')
            .some(item => item.trim().startsWith('agreement=1'));
    }

    function hideMessage(notificationElement) {
        notificationElement.classList.add('cookie-notification_hidden_yes');
    }

    function showMessage(notificationElement) {
        notificationElement.classList.remove('cookie-notification_hidden_yes');
    }

    function handleAgreementClick(event) {
        event.preventDefault();
        hideMessage(event.target.closest('.cookie-notification'));
        setAgreeCookie();
    }

    const notificationElement = createNotificationElement();
    document.body.appendChild(notificationElement);

    if (!checkAgreeCookie()) {
        showMessage(notificationElement);
    } else {
        hideMessage(notificationElement);
        setAgreeCookie(); // Обновляем куки, даже если пользователь уже согласился ранее
    }

    document.querySelector('#yes').addEventListener('click', handleAgreementClick);



    // Находим все элементы с классом .space
    const spaceElements = document.querySelectorAll('.space');

    // Для каждого элемента применяем значения из data-атрибутов
    spaceElements.forEach(space => {
        const height = space.getAttribute('data-height');
        const width = space.getAttribute('data-width');

        if (height) {
            space.style.setProperty('--space-height', height);
        }
        if (width) {
            space.style.setProperty('--space-width', width);
        }
    });

});