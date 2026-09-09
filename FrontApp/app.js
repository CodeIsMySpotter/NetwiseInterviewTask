const API = 'http://localhost:5205/facts';
const factsList = document.getElementById('factsList');
const status = document.getElementById('status');

async function loadFacts() {
    try {

        const res = await fetch(`${API}/file`);
        const text = await res.text();
        const facts = text.split('\n').filter(f => f.trim());

        if (facts.length === 0) {
            factsList.innerHTML = 'No facts yet.';
        } else {
            facts.reverse();
            
            let html = '';
            for (const fact of facts) {
                html += `<div class="fact-item">${fact}</div>`;
            }
            
            factsList.innerHTML = html;
        }

    } catch {
        factsList.innerHTML = 'Error loading facts.';
    }
}

document.getElementById('askBtn').addEventListener('click', async () => {
    status.textContent = 'Fetching...';
    
    try {
        await fetch(`${API}/ask`, { method: 'POST' });
        status.textContent = 'Fact added!';
        setTimeout(() => status.textContent = '', 2000);
        loadFacts();
    } catch {
        status.textContent = 'Error fetching fact.';
    }
});

loadFacts();
