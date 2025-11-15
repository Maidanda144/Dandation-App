// server.js (example)
const express = require('express');
const bodyParser = require('body-parser');
const twilio = require('twilio');
const app = express();
app.use(bodyParser.json());

const accountSid = process.env.TWILIO_SID;
const authToken = process.env.TWILIO_TOKEN;
const fromNumber = process.env.TWILIO_FROM;

const client = twilio(accountSid, authToken);

app.post('/api/send-sms', async (req, res) => {
  const { messages } = req.body; // [{name, class, status}]
  try {
    // Example: send one SMS per message (adjust as needed)
    const results = [];
    for (const m of messages) {
      const body = `${m.name} (${m.class}) est ${m.status} aujourd'hui.`;
      // client.messages.create({ body, from: fromNumber, to: '+233XXXXXXXXX' })
      // For demo we skip 'to' — you must map student -> phone number server-side.
      // results.push(await client.messages.create({ body, from: fromNumber, to: m.phone }));
    }
    res.json({ ok: true, sent: results.length });
  } catch (err) {
    console.error(err);
    res.status(500).send(err.message || 'twilio error');
  }
});

app.listen(3000);
