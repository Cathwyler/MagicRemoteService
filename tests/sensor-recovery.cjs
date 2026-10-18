const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const vm = require('node:vm');
const { test } = require('node:test');

const source = fs.readFileSync(path.join(__dirname, '../MagicRemoteService/Resources/TV/MagicRemoteService/main.js'), 'utf8');
const listeners = source.slice(source.indexOf('Window.prototype.oneEventListener'), source.indexOf('Object.prototype.spread'));
const subscription = source.slice(source.indexOf('function SubscriptionGetSensorData()'), source.indexOf('function LaunchInput()'));

function setup(version = 6) {
    class Target {
        constructor() { this.listeners = new Map(); }
        addEventListener(type, handler) {
            if (!this.listeners.has(type)) this.listeners.set(type, new Set());
            this.listeners.get(type).add(handler);
        }
        removeEventListener(type, handler) { this.listeners.get(type)?.delete(handler); }
        dispatch(type, event) {
            for (const handler of [...(this.listeners.get(type) || [])]) handler.call(this, event);
        }
    }
    const requests = [];
    const errors = [];
    const context = vm.createContext({
        Window: Target, Document: Target, Element: Target,
        window: new Target(), document: new Target(), arrVersion: [version],
        webOS: { service: { request(uri, options) { requests.push(options); } } },
        oString: { strGetSensorDataFailure: 'Sensor failure' },
        LogIfDebug() {}, Error(...args) { errors.push(args); }
    });
    vm.runInContext(listeners + subscription, context);
    context.SubscriptionGetSensorData();
    return { context, requests, errors };
}

for (const code of ['1003', 1003, '1301']) {
    test(`sleep error ${JSON.stringify(code)} waits for pointer and retries once`, () => {
        const { context, requests, errors } = setup();
        requests[0].onFailure({ errorCode: code, errorText: 'Magic Remote is not Ready' });
        assert.equal(errors.length, 0);
        assert.equal(requests.length, 1);
        context.document.dispatch('cursorStateChange', { detail: { visibility: false } });
        assert.equal(requests.length, 1);
        context.document.dispatch('cursorStateChange', { detail: { visibility: true } });
        assert.equal(requests.length, 2);
        context.document.dispatch('cursorStateChange', { detail: { visibility: true } });
        assert.equal(requests.length, 2);
    });
}

test('another 1003 failure remains visible and does not retry', () => {
    const { context, requests, errors } = setup();
    requests[0].onFailure({ errorCode: '1003', errorText: 'Subscription add fail' });
    context.document.dispatch('cursorStateChange', { detail: { visibility: true } });
    assert.equal(errors.length, 1);
    assert.equal(requests.length, 1);
});

test('old webOS retries only on its pointer activation key', () => {
    const { context, requests } = setup(2);
    requests[0].onFailure({ errorCode: '1003', errorText: 'Magic Remote is not Ready' });
    context.window.dispatch('keydown', { keyCode: 13 });
    assert.equal(requests.length, 1);
    context.window.dispatch('keydown', { keyCode: 0x600 });
    assert.equal(requests.length, 2);
    context.window.dispatch('keydown', { keyCode: 0x600 });
    assert.equal(requests.length, 2);
});

test('remote still asleep after a retry waits for another wake event', () => {
    const { context, requests } = setup();
    const error = { errorCode: '1003', errorText: 'Magic Remote is not Ready' };
    requests[0].onFailure(error);
    context.document.dispatch('cursorStateChange', { detail: { visibility: true } });
    requests[1].onFailure(error);
    assert.equal(requests.length, 2);
    context.document.dispatch('cursorStateChange', { detail: { visibility: false } });
    context.document.dispatch('cursorStateChange', { detail: { visibility: true } });
    assert.equal(requests.length, 3);
});
